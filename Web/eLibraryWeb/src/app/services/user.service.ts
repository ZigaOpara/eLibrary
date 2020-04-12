import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/User';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private root = 'api/user';

  constructor(private router: Router, private http: HttpClient) {}

  logIn(user: User, role: string) {
    user.role = role;
    localStorage.setItem('user', JSON.stringify(user));
    this.router.navigate(['/home']).then((_) => null);
  }

  logOut() {
    localStorage.removeItem('user');
    this.router.navigate(['/login']).then((_) => null);
  }

  isLoggedIn() {
    return localStorage.getItem('user');
  }

  getCurrentUser() {
    const user = JSON.parse(localStorage.getItem('user'));
    if (!user) {
      this.router.navigate(['/login']).then((_) => null);
    }
    return user;
  }

  getUsers() {
    return this.http.get<User[]>(this.root).pipe();
  }

  authenticate(username: string): Observable<User> {
    const user = new User();
    user.username = username;
    return this.http.post<User>(this.root, user).pipe();
  }
}
