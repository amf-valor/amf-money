import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  constructor(private snackBarRef: MatSnackBar) { }

  showGenericError(){
    this.snackBarRef.open(
      'Oops! Houve um problema com nossos servidores tente novamente mais tarde!', 
      'entendi!', { duration: 5000})
  }
}
