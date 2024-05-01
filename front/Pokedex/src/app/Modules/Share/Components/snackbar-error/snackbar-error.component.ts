import {Component, inject, Inject} from '@angular/core';
import {MAT_SNACK_BAR_DATA, MatSnackBarRef} from "@angular/material/snack-bar";

@Component({
  selector: 'app-snackbar-error',
  templateUrl: './snackbar-error.component.html',
  styleUrl: './snackbar-error.component.scss'
})
export class SnackbarErrorComponent {
  messages: string[] = this.data.messages;
  snackBarRef = inject(MatSnackBarRef);

  constructor(
    @Inject(MAT_SNACK_BAR_DATA) public data: { messages: string[] }) {
  }
}
