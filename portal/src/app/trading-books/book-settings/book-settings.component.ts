import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'amp-book-settings',
  templateUrl: './book-settings.component.html',
  styleUrls: ['./book-settings.component.css']
})
export class BookSettingsComponent implements OnInit {

  bookSettingsForm: FormGroup;
  
  constructor(public dialogRef: MatDialogRef<BookSettingsComponent>, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.bookSettingsForm = this.formBuilder.group({
      bookName: this.formBuilder.control('', [Validators.required]),
      amountPerCaptal: this.formBuilder.control('', [Validators.required]),
      riskGainRelationship: this.formBuilder.control('', Validators.required)
    })
  }

  onNoBtnClick(): void{
    this.dialogRef.close();
  }

}
