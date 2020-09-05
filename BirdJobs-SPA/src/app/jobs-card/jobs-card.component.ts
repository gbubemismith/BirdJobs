import { Component, OnInit, Input } from '@angular/core';
import { Results } from '../models/jobsModel';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-jobs-card',
  templateUrl: './jobs-card.component.html',
  styleUrls: ['./jobs-card.component.css']
})
export class JobsCardComponent implements OnInit {

  @Input('job') job: Results;
  closeResult = '';

  constructor(private modalService: NgbModal) { }

  ngOnInit() {
    
  }


  open(content) {

    console.log('Results in modal', this.job);

    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title', size: 'lg'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

}
