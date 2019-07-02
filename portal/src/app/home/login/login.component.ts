import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'amp-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent{

  loginForm: FormGroup
  controlErrorMessages: Map<string, Map<string, string>>
  
  constructor(private formBuilder: FormBuilder, private messageService: MessageService) { 
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

  get passwordMinLength(): number {
    return 8;
  }

  get email(): string{
    return "email"
  }

  get password(): string{
    return "password"
  }
}
