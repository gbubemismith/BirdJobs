import { Results } from './../models/jobsModel';
import { LoadingService } from './../services/loading-service.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { TwitterFunctionsService } from './../services/twitter-functions.service';
import { Component, OnInit } from '@angular/core';
import { map, finalize } from 'rxjs/operators';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  next: string = '';
  skeletonloader = true;
  closeResult = '';

  jobsArray: BehaviorSubject<Results[]> = new BehaviorSubject<Results[]>([]);
  jobs$: Observable<Results[]> = this.jobsArray.asObservable();


  constructor(private twitterFunctions: TwitterFunctionsService, private loadingService: LoadingService) { }

  ngOnInit() {

    // this.twitterFunctions.loadAllJobs().subscribe(res => {

    //   this.jobs = res["results"];
    //   this.next = res.next;
    //   this.skeletonloader = false;

    // }, error => console.log('Something happened'));

   this.loadJobsReactive();
              
  }

  loadJobsReactive() {
    this.loadingService.loadingOn();

     //reactive approach
    //  this.jobs$ = this.twitterFunctions.loadAllJobs()
    //  .pipe(
    //    map(response => {
    //      return response["results"]
    //    }),
    //    finalize(() => this.loadingService.loadingOff())
    //  );

    const jobsResults$ = this.twitterFunctions.loadAllJobs();

     const loadJobs$ = this.loadingService.showLoadingUntilCompleted(jobsResults$);

    //  this.jobs$ = loadJobs$
    //                       .pipe(
    //                         map(
    //                           response => {
    //                             this.next = response.next;
    //                             return response.results;
    //                           }
    //                         )
    //                       );

    loadJobs$.subscribe(response => {
      console.log(response);
      this.next = response.next;
      this.jobsArray.next(response.results);
    });


      

  }

  displayMoreJobs() {
    console.log('clicked');

    this.twitterFunctions.loadAllJobs(this.next)
                                            .pipe(
                                              map(
                                                response => {
                                                  this.next = response.next;
                                                  return response.results;
                                                }
                                              )
                                            )
                                            .subscribe(response => {
                                              const currentValue = this.jobsArray.value;
                                              currentValue.push(...response);
                                              console.log('check', currentValue);
                                            });

    
    // const currentValue = this.jobsArray.value;
    // // currentValue.push(this.nextJobs);

    // console.log('Current', this.nextJobs);
    
    // this.jobsArray.next(currentValue);

    
  }

  


}
