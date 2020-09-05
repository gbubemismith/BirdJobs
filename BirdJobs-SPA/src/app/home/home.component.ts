import { LoadingService } from './../services/loading-service.service';
import { Observable } from 'rxjs';
import { TwitterFunctionsService } from './../services/twitter-functions.service';
import { Component, OnInit } from '@angular/core';
import { Results } from '../models/jobsModel';
import { map, finalize } from 'rxjs/operators';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  next: string = '';
  jobs: Results[];
  skeletonloader = true;
  closeResult = '';

  jobs$: Observable<Results[]>;


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
    //  .pipe(
    //    map(response => response
    //    )
    //  );

     const loadJobs$ = this.loadingService.showLoadingUntilCompleted(jobsResults$);

     this.jobs$ = loadJobs$
                          .pipe(
                            map(
                              response => {
                                this.next = response.next;
                                return response.results;
                              }
                            )
                          );

  }

  


}
