import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";

//import 'rxjs/add/operator/map';

@Injectable()
export class AppService {
  constructor(private http: Http) {

  }
  getItems() {
    let apiUrl = 'api/users'
    return this.http.get(apiUrl).pipe(map(response => { return response.json()}));
  }
}
