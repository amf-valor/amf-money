import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MessageService } from 'src/app/services/message.service';
import { PortalApiService } from 'src/app/services/portal-api.service';
import { Account } from '../sign-up/account.model'
import { UtilsService } from 'src/app/services/utils.service';
import { Router } from '@angular/router';

@Component({
  selector: 'amp-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  signUpForm: FormGroup
  controlErrorMessages: Map<string, Map<string, string>>

  constructor(private formBuilder: FormBuilder, 
    private messageService: MessageService,
    private portalApiService: PortalApiService,
    private utilsService: UtilsService,
    private router: Router) { }

  ngOnInit() {
    this.initSignUpForm()
    this.initControlErrorMessages()
  }

  private initSignUpForm() : void{
    this.signUpForm = this.formBuilder.group({
      dateOfBirth: this.formBuilder.control('', [Validators.required]),
      email: this.formBuilder.control('', [Validators.required, Validators.email]),
      password: this.formBuilder.control('', [
        Validators.required, 
        Validators.minLength(this.passwordMinLength)
      ]),
      pin: this.formBuilder.control('', [
        Validators.required, 
        Validators.minLength(this.pinLength),
        Validators.maxLength(this.pinLength)
      ])
    })
  }

  private initControlErrorMessages() : void{
    this.controlErrorMessages = new Map<string, Map<string, string>>();
    this.controlErrorMessages.set(this.dateOfBirth, new Map<string, string>([
      ['required', this.messageService.get('signUp.dateOfBirth.required')]
    ]))
    this.controlErrorMessages.set(this.email, new Map<string, string>([
      ['required', this.messageService.get('signUp.email.required')],
      ['email', this.messageService.get('signUp.email.emailError')]
    ]))
    this.controlErrorMessages.set(this.password, new Map<string, string>([
      ['required', this.messageService.get('signUp.password.required')],
      ['minlength', 
        `${this.messageService.get('signUp.password.minlength')} 
        ${this.passwordMinLength}`]
    ]))
    this.controlErrorMessages.set(this.pin, new Map<string, string>([
      ['required', this.messageService.get('signUp.pin.required')],
      ['minlength', 
        `${this.messageService.get('signUp.pin.minlength')} 
         ${this.pinLength} ${this.messageService.get('signUp.pin.minlength2')}`],
      ['maxlength', `${this.messageService.get('signUp.pin.maxlength')} 
      ${this.pinLength} ${this.messageService.get('signUp.pin.maxlength2')}`]
    ]))

  }

  get dateOfBirth(): string {
    return "dateOfBirth"
  }

  get email() : string{
    return "email"
  }

  get password() : string{
    return "password"
  }

  get passwordMinLength() : number{
    return 8
  }

  get pin() : string{
    return "pin"
  }

  get pinLength () : number{
    return 4
  }

  onSignUpButtonClick(): void{
    const account: Account = {
      birth: this.signUpForm.get(this.dateOfBirth).value,
      email: this.signUpForm.get(this.email).value,
      password: this.signUpForm.get(this.password).value,
      pin: this.signUpForm.get(this.pin).value 
    }
    
    this.portalApiService.postAccount(account)
        .subscribe(() => {
          this.router.navigate(['./tradingBooks']);
        }, err => {
          if(err.status == 409){
            this.utilsService.showMessage(this.messageService.get('signUp.email.alreadyExists'))
          }else{
            this.utilsService.showNetworkError()
          }
        }) 
  }

}
