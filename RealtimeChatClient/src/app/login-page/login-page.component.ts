import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../services/auth.service';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {LoginUserModel} from '../interfaces/login-user.model';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  form: FormGroup;
  submitted = false;
  message;
  hide = true;

  constructor(
    private auth: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params: Params) => {
      if (params.loginAgain) {
        // this.message = 'Please, login as admin';
        this.message = 'Session expired. Please, input your credentials again.';
      } else if (params.authFailed) {
        this.message = 'Session expired. Please, input your credentials again.';
      }
    });

    this.form = new FormGroup({
      email: new FormControl(null,
        [Validators.required, Validators.email]),
      password: new FormControl(null,
        [Validators.required, Validators.minLength(8)])
    });
    localStorage.clear();
  }

  submit(): void {
    if (this.form.invalid){
      return;
    }
    this.submitted = true;

    const user: LoginUserModel = {
      email: this.form.value.email,
      password: this.form.value.password
    };

    this.auth.login(user).subscribe(() => {
      // this.form.reset();
      // this.router.navigate(['chat']);
      this.submitted = false;
    }, (error: HttpErrorResponse) => {
      console.log(error.error);
      this.submitted = false;
    });
  }

}
