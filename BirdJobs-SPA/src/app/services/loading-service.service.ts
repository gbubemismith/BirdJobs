import { BehaviorSubject, Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';
import { tap, concatMap, finalize } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  private loadingSubject = new BehaviorSubject<boolean>(false);

  loading$: Observable<boolean> = this.loadingSubject.asObservable();

  showLoadingUntilCompleted<T>(obs$: Observable<T>) : Observable<T> {
    return of(null)
              .pipe(
                tap(() => this.loadingOn()),
                concatMap(() => obs$),
                finalize(() => this.loadingOff())
              );
  }

  loadingOn() {
    this.loadingSubject.next(true);
  }

  loadingOff() {
    this.loadingSubject.next(false);
  }


}
