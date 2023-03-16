import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

export interface IPerson {
  personId : number;
  name : string;
  addressLine1 : string;
  addressLine2 : string;
  city : string;
  state : string;
  zip : string;
}

export class PersonDto {
  personId : number;
  name : string;
  addressLine1 : string;
  addressLine2 : string;
  city : string;
  state : string;
  zip : string;
}

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private apiUrl : string;

  constructor (private http : HttpClient, @Inject('BASE_URL') baseUrl : string) {
    this.apiUrl = baseUrl + 'api/private/person/';
  }

  // GET: api/private/person
  getPeople () {
    return this.http.get<IPerson[]>(this.apiUrl);
  }

  // PUT: api/private/person
  addPerson (personDto : PersonDto) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    return this.http.put<IPerson[]>(this.apiUrl, personDto, httpOptions);
  }

  // POST: api/private/person/update
  updatePerson (person : IPerson) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    return this.http.post<IPerson[]>(this.apiUrl + 'update', person, httpOptions);
  }

  // DELETE: api/private/person/{personId}
  deletePerson (personId: number) {
    return this.http.delete<IPerson[]>(this.apiUrl + personId);
  }
}
