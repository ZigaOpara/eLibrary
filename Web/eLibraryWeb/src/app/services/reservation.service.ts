import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Reservation } from '../models/Reservation';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  private root = 'api/reservation';

  constructor(private http: HttpClient) {}

  getReservations(): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(this.root).pipe();
  }

  getReservationsForUser(id: number): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(`${this.root}/${id}`).pipe();
  }

  addReservation(reservation: Reservation): Observable<Reservation> {
    return this.http.post<Reservation>(this.root, reservation).pipe();
  }

  removeReservation(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.root}/${id}`).pipe();
  }
}
