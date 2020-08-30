import { Component, OnInit, Input } from '@angular/core';
import { Results } from '../models/jobsModel';

@Component({
  selector: 'app-jobs-card',
  templateUrl: './jobs-card.component.html',
  styleUrls: ['./jobs-card.component.css']
})
export class JobsCardComponent implements OnInit {
  @Input('job') job: Results;

  constructor() { }

  ngOnInit() {
    
  }

}
