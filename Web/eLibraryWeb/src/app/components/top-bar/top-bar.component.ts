import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../../services/data.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-top-bar',
  templateUrl: './top-bar.component.html',
  styleUrls: ['./top-bar.component.scss'],
})
export class TopBarComponent implements OnInit {
  currentUser: string;

  constructor(
    private router: Router,
    private userService: UserService,
    private dataService: DataService
  ) {}

  ngOnInit(): void {
    this.dataService.currentUser.subscribe(
      (currentUser) => (this.currentUser = currentUser)
    );
    if (!this.currentUser) {
      this.getCurrentUser();
    }
  }

  getCurrentUser() {
    this.currentUser = JSON.parse(localStorage.getItem('user')).username;
  }

  logOut() {
    this.getCurrentUser();
    this.userService.logOut();
    this.dataService.changeMessage('');
  }
}
