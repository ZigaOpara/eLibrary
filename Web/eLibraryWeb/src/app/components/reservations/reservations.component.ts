import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../../services/reservation.service';
import { UserService } from '../../services/user.service';
import { User } from '../../models/User';
import { Reservation } from '../../models/Reservation';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/Book';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss'],
})
export class ReservationsComponent implements OnInit {
  currentUser: User;
  reservations: Reservation[];
  books: Book[];
  users: User[];
  r1: Reservation2[] = [];

  constructor(
    private reservationService: ReservationService,
    private userService: UserService,
    private bookService: BookService
  ) {}

  ngOnInit(): void {
    this.getCurrentUser();
    this.getBooks();
  }

  getCurrentUser() {
    this.currentUser = this.userService.getCurrentUser();
  }

  getReservations() {
    if (this.currentUser.role === 'user') {
      this.getReservationsForUser(this.currentUser.id);
    } else if (this.currentUser.role === 'admin') {
      this.getAllReservations();
    }
  }

  getReservationsForUser(id: number) {
    this.reservationService
      .getReservationsForUser(id)
      .subscribe((reservations) => {
        this.reservations = reservations;
        this.mapReservations();
      });
  }

  getAllReservations() {
    this.reservationService.getReservations().subscribe((reservations) => {
      this.reservations = reservations;
      this.mapReservations();
    });
  }

  removeReservation(id: number) {
    this.reservationService
      .removeReservation(id)
      .subscribe((_) => this.getBooks());
  }

  getBooks() {
    this.bookService.getBooks().subscribe((books) => {
      this.books = books;
      this.getUsers();
    });
  }

  getUsers() {
    this.userService.getUsers().subscribe((users) => {
      this.users = users;
      this.getReservations();
    });
  }

  mapReservations() {
    this.r1 = [];
    for (const reservation of this.reservations) {
      this.r1.push({
        id: reservation.id,
        user: this.users.find((user) => user.id === reservation.userId)
          .username,
        book: this.books.find((book) => book.id === reservation.bookId).title,
      });
    }
  }
}

export class Reservation2 {
  id: number;
  user: string;
  book: string;
}
