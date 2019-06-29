import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from './input/input.component';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatToolbarModule, MatDialogModule, MatInputModule, MatSnackBarModule, MatSelect, MatSelectModule, MatIconModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MoneyLabelComponent } from './money-label/money-label.component';

@NgModule({
  declarations: [InputComponent, MoneyLabelComponent],
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
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    InputComponent,
    MoneyLabelComponent,
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
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class SharedModule { }
