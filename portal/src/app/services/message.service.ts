import { Injectable, Inject, LOCALE_ID } from '@angular/core';
import { Messages } from 'src/locale/messages';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(@Inject(LOCALE_ID) private localeId: string) { }

  get(messageId: string): string{

    if(this.localeId == 'en-US') 
      return Messages.enUS.get(messageId)

    if(this.localeId == 'pt-BR') 
      return Messages.ptBR.get(messageId)
    
    return "no translation found"
  }
}
