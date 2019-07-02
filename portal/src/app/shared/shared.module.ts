import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from './input/input.component';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatToolbarModule, MatDialogModule, MatInputModule, MatSnackBarModule, MatSelect, MatSelectModule, MatIconModule, MatTabsModule, MatCardModule, MatDatepicker, MatDatepickerModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MoneyLabelComponent } from './money-label/money-label.component';
import { MatMomentDateModule } from "@angular/material-moment-adapter";
import { InputDateComponent } from './input-date/input-date.component';

@NgModule({
  declarations: [InputComponent, MoneyLabelComponent, InputDateComponent],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatToolbarModule,
    FlexLayoutModule,
    MatDialogModule,
    MatInputModule,
    MatSnackBarModule,
    MatSelectModule,
    MatIconModule,
    MatTabsModule,
    MatMomentDateModule,
    FormsModule,
    MatCardModule,
    MatDatepickerModule,
    ReactiveFormsModule
  ],
  exports: [
    InputComponent,
    MoneyLabelComponent,
    InputDateComponent,
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatToolbarModule,
    FlexLayoutModule,
    MatDialogModule,
    MatInputModule,
    MatSnackBarModule,
    MatSelectModule,
    MatIconModule,
    MatTabsModule,
    MatDatepickerModule,
    MatMomentDateModule,
    FormsModule,
    MatCardModule,
    ReactiveFormsModule,
  ]
})
export class SharedModule { }
