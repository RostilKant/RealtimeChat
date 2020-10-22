import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../services/auth.service';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {RegistrationUserModel} from '../interfaces/registration-user.model';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.scss']
})
export class RegisterPageComponent implements OnInit {

  form: FormGroup;
  submitted = false;
  message;
  hide = true;

  constructor(
    private auth: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder
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

    this.form = this.fb.group({
      username: [null, [Validators.required, Validators.pattern(/\d/)]],
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(8)]],
    });
    localStorage.clear();
  }

  submit(): void {
    if (this.form.invalid){
      return;
    }

    this.submitted = true;

    const user: RegistrationUserModel = {
      username: this.form.value.username,
      email: this.form.value.email,
      password: this.form.value.password,
      roles: ['User']
    };

    this.auth.register(user).subscribe(() => {
      this.form.reset();
      this.router.navigate(['login']);
      this.submitted = false;
    }, (error: HttpErrorResponse) => {
      this.message = error.error?.DuplicateEmail;
      this.message = error.error?.error;
      this.submitted = false;
    });
  }

}
