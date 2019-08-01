import { Component, Output, EventEmitter} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MessageService } from 'src/app/services/message.service';
import { Credentials } from './credentials.model';
import { Router } from '@angular/router';
import { UtilsService } from 'src/app/services/utils.service';
import { AuthenticationService } from 'src/app/shared/authentication.service';
import { finalize } from 'rxjs/operators';
import { MatDialog } from '@angular/material';
import { ForgotPasswordComponent } from '../forgot-password/forgot-password.component';
@Component({
  selector: 'amp-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent{
  loginForm: FormGroup
  controlErrorMessages: Map<string, Map<string, string>>

  @Output() onLoginStarted =  new EventEmitter();
  @Output() onLoginFinished =  new EventEmitter();

  get passwordMinLength(): number {
    return 8;
  }

  get email(): string{
    return "email"
  }

  get password(): string{
    return "password"
  }
  
  constructor(
    private formBuilder: FormBuilder, 
    private messageService: MessageService,
    private authenticationService: AuthenticationService,
    private router: Router,
    private utilsService: UtilsService,
    public dialog: MatDialog){
      if(this.authenticationService.currentToken){
        this.router.navigate(['./tradingBooks'])
      }

      this.initLoginForm()
      this.initControlErrorMessages()
  }

  private initLoginForm() : void{
    this.loginForm = this.formBuilder.group({
      email: this.formBuilder.control('', [Validators.required, Validators.email]),
      password: this.formBuilder.control('', [
        Validators.required, Validators.minLength(this.passwordMinLength)
      ])
    })
  }

  private initControlErrorMessages() : void{
    this.controlErrorMessages = new Map<string, Map<string, string>>();
    this.controlErrorMessages.set(this.email, new Map<string, string>([
      ['required', this.messageService.get('login.email.required')],
      ['email', this.messageService.get('login.email.emailError')]
    ]))
    this.controlErrorMessages.set(this.password, new Map<string, string>([
      ['required', this.messageService.get('login.password.required')],
      ['minlength', `${this.messageService.get('login.password.minlength')} ${this.passwordMinLength}`]
    ]))
  }

  onLoginButtonClick(): void{
    const credentials: Credentials = {
      email: this.loginForm.get(this.email).value,
      password: this.loginForm.get(this.password).value, 
    }

    this.onLoginStarted.emit();

    this.authenticationService.postCredentials(credentials)
      .pipe(
        finalize(() => this.onLoginFinished.emit())
      )
      .subscribe(() => {
        this.router.navigate(['./tradingBooks']);
      }, err => {
        if(err.status == 401){
          this.utilsService.showMessage(this.messageService.get('login.wrongCredentials'))
        }else{
          this.utilsService.showNetworkError()
        }
      })  
  }

  onForgotPaswwordButtonClick(): void{
    this.dialog.open(ForgotPasswordComponent);
  }
}
