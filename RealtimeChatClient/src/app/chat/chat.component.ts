import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {SignalrService} from '../services/signalr.service';
import {UserMessageModel} from '../interfaces/user-message.model';
import {UserService} from '../services/user.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  form: FormGroup;
  submitted = false;
  username = localStorage.getItem('username');

  constructor(
    public signalrService: SignalrService,
    private userService: UserService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      message: [null]
    });
    this.signalrService.messages = [];

    this.signalrService.startConnection();
    this.signalrService.addMessageListener();
    this.signalrService.addNotifyListener();
  }

  submit(): void {
    if (this.form.invalid){
      return;
    }
    this.submitted = true;

    const userMessage: UserMessageModel = {
      username: this.username,
      message: this.form.get('message').value
    };

   /* this.userService.getUsername().subscribe(username => {
      userMessage.username = username;
    });
    userMessage.message = this.form.get('message').value;*/

    this.signalrService.SendMessageToAll(userMessage);

    this.form.reset();
    this.submitted = false;

  }

  isMyMessage(messageUsername: string): boolean{
    return this.username === messageUsername;
  }
}
