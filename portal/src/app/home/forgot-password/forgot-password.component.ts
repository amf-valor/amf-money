import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'amp-forgot-password',
  templateUrl: './forgot-password.component.html'
})
export class ForgotPasswordComponent{

  constructor(
    public dialogRef: MatDialogRef<ForgotPasswordComponent>) {}

  onOkClick(): void {
    this.dialogRef.close();
  }


}
