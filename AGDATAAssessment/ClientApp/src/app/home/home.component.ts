import { Component, OnInit, ViewChild } from '@angular/core';
import { PersonService, IPerson, PersonDto } from '../../services/person.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { faTriangleExclamation } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  active = 1;
  people : IPerson[];
  selectedPerson : IPerson;
  newPerson : PersonDto;
  showMessageBox : boolean = false;
  messageBoxType : string = 'danger';
  message : string;
  faUser = faUser;
  faTriangleExclamation = faTriangleExclamation;

  @ViewChild('messageBox', { static: false }) messageBox : NgbAlert;

  constructor (private personService : PersonService, private modalService : NgbModal) { }

  ngOnInit () {
    this.getPeople();
  }

  getPeople () {
    this.personService.getPeople().subscribe(result => {
      this.people = result;
    }, error => {
      this.message = error.error;
      this.messageBoxType = 'danger';
      this.showMessageBox = true;
      setTimeout(() => this.messageBox.close(), 5000);
    })
  }

  addPerson () {
    this.personService.addPerson(this.newPerson).subscribe(result => {
      this.people = result;
      this.message = 'Person successfully added';
      this.messageBoxType = 'success';
      this.showMessageBox = true;
    }, error => {
      console.log('Error: ', error);
      this.message = error ? error.error
        : 'Some unknown issue occurred. Please try again.';
      this.messageBoxType = 'danger';
      this.showMessageBox = true;
    });

    setTimeout(() => this.messageBox.close(), 5000);

    this.modalService.dismissAll();
  }

  updatePerson () {
    this.personService.updatePerson(this.selectedPerson).subscribe(result => {
      this.people = result;
      this.message = 'Person successfully updated';
      this.messageBoxType = 'success';
      this.showMessageBox = true;
    }, error => {
      this.message = error ? error.error
        : 'Some unknown issue occurred. Please try again.';
      this.messageBoxType = 'danger';
      this.showMessageBox = true;
    });

    setTimeout(() => this.messageBox.close(), 5000);

    this.modalService.dismissAll();
  }

  deletePerson () {
    this.personService.deletePerson(this.selectedPerson.personId).subscribe(result => {
      this.people = result;
      this.message = 'Person successfully deleted';
      this.messageBoxType = 'success';
      this.showMessageBox = true;
    }, error => {
      this.message = error ? error.error
        : 'Some unknown issue occurred. Please try again.';
      this.messageBoxType = 'danger';
      this.showMessageBox = true;
    });

    setTimeout(() => this.messageBox.close(), 5000);

    this.modalService.dismissAll();
  }

  openModal (modal, person : IPerson) {
    if (person) {
      this.selectedPerson = person;
    }
    else {
      this.newPerson = new PersonDto();
    }

    this.modalService.open(modal);
  }
}
