import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import {UserMessageModel} from '../interfaces/user-message.model';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  public messages: UserMessageModel[] = [];
  private hubConnection: signalR.HubConnection;

  constructor() { }

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/chat')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection Started'))
      .catch(error => console.log(error));
  }

  public SendMessageToAll(message: UserMessageModel): void {
    this.hubConnection.invoke('send', message)
      .catch(err => console.log(err));
  }

  public addMessageListener(): void {
    this.hubConnection.on('send', (message: UserMessageModel) => {
      this.messages.unshift(message);
    });
  }
}
