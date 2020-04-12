import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm;
  currentRole: string;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private dataService: DataService,
    private userService: UserService
  ) {
    this.loginForm = this.formBuilder.group({
      username: this.formBuilder.control(null, Validators.required),
    });
  }

  ngOnInit(): void {}

  onSubmit(data) {
    this.userService.authenticate(data.username).subscribe((user) => {
      this.userService.logIn(user, this.currentRole);
      this.dataService.changeMessage(user.username);
    });
  }

  setRole(role: string) {
    this.currentRole = role;
  }
}
