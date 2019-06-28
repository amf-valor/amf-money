import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  constructor(private snackBarRef: MatSnackBar, private messageService: MessageService) { }

  showNetworkError(){
    this.snackBarRef.open(
      this.messageService.get('notification.network.error'), 
      this.messageService.get('notification.agree'), { duration: 5000})
  }

  showMessage(message: string){
    this.snackBarRef.open(
      message, 'Ok', { duration: 5000})
  }
}
