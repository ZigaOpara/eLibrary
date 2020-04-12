import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../models/Book';
import { Rate } from '../models/Rate';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private root = 'api/book';

  constructor(private http: HttpClient) {}

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.root).pipe();
  }

  addBook(book: Book): Observable<Book> {
    console.log('adding book');
    return this.http.post<Book>(this.root, book).pipe();
  }

  editBook(book: Book): Observable<Book> {
    return this.http.put<Book>(this.root, book).pipe();
  }

  deleteBook(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.root}/${id}`).pipe();
  }

  addRating(rate: Rate): Observable<Rate> {
    return this.http.post<Rate>(`${this.root}/rating`, rate).pipe();
  }
}
