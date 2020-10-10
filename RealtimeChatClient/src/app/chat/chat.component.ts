import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {SignalrService} from '../services/signalr.service';
import {UserMessageModel} from '../interfaces/user-message.model';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  form: FormGroup;
  submitted = false;

  constructor(
    public signalrService: SignalrService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      username: [null],
      message: [null]
    });

    this.signalrService.startConnection();
    this.signalrService.addMessageListener();
  }

  submit(): void {
    if (this.form.invalid){
      return;
    }
    this.submitted = true;

    const userMessage: UserMessageModel = {
      username: this.form.value.username,
      message: this.form.value.message
    };
    this.signalrService.SendMessageToAll(userMessage);

    this.submitted = false;

  }
}
