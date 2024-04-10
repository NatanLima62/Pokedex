import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  @Output() signalChange = new EventEmitter<boolean>();
  signalToggle = false;

  emitSignal() {
    this.signalToggle = !this.signalToggle;
    this.signalChange.emit(this.signalToggle);
  }
}
