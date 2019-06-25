import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  constructor(private snackBarRef: MatSnackBar) { }

  showNetworkError(){
    this.snackBarRef.open(
      'Oops! Houve um problema com nossos servidores tente novamente mais tarde!', 
      'entendi!', { duration: 5000})
  }

  showMessage(message: string){
    this.snackBarRef.open(
      message, 'Ok', { duration: 5000})
  }
}
