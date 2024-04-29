import {Component, inject, Inject, OnInit} from '@angular/core';
import {MAT_SNACK_BAR_DATA, MatSnackBarRef} from "@angular/material/snack-bar";

@Component({
  selector: 'app-erro',
  templateUrl: './erro.component.html',
  styleUrl: './erro.component.scss'
})
export class ErroComponent{
  messages: string[] = this.data.messages;
  snackBarRef = inject(MatSnackBarRef);

  constructor(
    @Inject(MAT_SNACK_BAR_DATA) public data: { messages: string[] }) {
  }
}
