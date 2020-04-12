import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/Book';
import { UserService } from '../../services/user.service';
import { User } from '../../models/User';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReservationService } from '../../services/reservation.service';
import { Reservation } from '../../models/Reservation';
import { Rate } from '../../models/Rate';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  books: Book[];
  reservations: Reservation[];
  user: User;
  currentBook = new Book();
  addBookForm: FormGroup;
  editBookForm: FormGroup;
  loading: boolean;
  searchText = '';

  constructor(
    private bookService: BookService,
    private userService: UserService,
    private reservationService: ReservationService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.user = this.userService.getCurrentUser();
    this.loading = true;
    this.reservationService
      .getReservationsForUser(this.user.id)
      .subscribe((reservations) => {
        this.reservations = reservations;
        this.getBooks();
      });

    this.addBookForm = this.formBuilder.group({
      title: this.formBuilder.control(null, Validators.required),
      author: this.formBuilder.control(null, Validators.required),
      stock: this.formBuilder.control(null, Validators.required),
    });

    this.editBookForm = this.formBuilder.group({
      title: this.formBuilder.control(
        this.currentBook.title,
        Validators.required
      ),
      author: this.formBuilder.control(
        this.currentBook.author,
        Validators.required
      ),
      stock: this.formBuilder.control(
        this.currentBook.stock,
        Validators.required
      ),
    });
  }

  getBooks() {
    this.bookService.getBooks().subscribe((res) => {
      this.books = res;
      this.loading = false;
    });
  }

  deleteBook(id: number) {
    this.bookService.deleteBook(id).subscribe((_) => this.getBooks());
  }

  isLoggedIn() {
    return this.userService.isLoggedIn();
  }

  open(content) {
    this.modalService.open(content);
  }

  openEdit(content, book: Book) {
    this.currentBook = book;
    this.editBookForm.controls.title.setValue(this.currentBook.title);
    this.editBookForm.controls.author.setValue(this.currentBook.author);
    this.editBookForm.controls.stock.setValue(this.currentBook.stock);
    this.editBookForm.markAsPristine();
    this.modalService.open(content);
  }

  addBook(data) {
    this.bookService.addBook(data as Book).subscribe((_) => this.getBooks());
    this.addBookForm.reset();
    this.modalService.dismissAll();
  }

  editBook(data) {
    const book = new Book();
    book.id = this.currentBook.id;
    book.title = this.editBookForm.controls.title.value;
    book.author = this.editBookForm.controls.author.value;
    book.stock = this.editBookForm.controls.stock.value;
    this.bookService.editBook(book).subscribe((_) => this.getBooks());
    this.editBookForm.reset();
    this.modalService.dismissAll();
  }

  getReservationsForUser(id: number) {
    this.loading = true;
    this.reservationService
      .getReservationsForUser(id)
      .subscribe((reservations) => {
        this.reservations = reservations;
        this.getBooks();
      });
  }

  addReservation(id: number) {
    const reservation = new Reservation();
    reservation.bookId = id;
    reservation.userId = this.user.id;
    this.reservationService
      .addReservation(reservation)
      .subscribe((_) => this.getReservationsForUser(this.user.id));
  }

  removeReservation(bookId: number) {
    const removedReservation = this.reservations.find(
      (reservation) =>
        reservation.bookId === bookId && reservation.userId === this.user.id
    );
    this.reservationService
      .removeReservation(removedReservation.id)
      .subscribe((_) => this.getReservationsForUser(this.user.id));
  }

  isReserved(bookId: number): boolean {
    const tmp = this.reservations.find(
      (reservation) =>
        reservation.bookId === bookId && reservation.userId === this.user.id
    );
    return !!tmp;
  }

  addRating(rating: number, bookId: number) {
    const rate = new Rate();
    rate.rating = rating;
    rate.bookId = bookId;
    rate.userId = this.user.id;
    this.bookService.addRating(rate).subscribe((_) => this.getBooks());
  }
}
