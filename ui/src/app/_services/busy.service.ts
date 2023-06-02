import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;

  constructor(private ss: NgxSpinnerService) { }

  busy(){
    this.busyRequestCount++;
    this.ss.show(undefined,{
      type: "ball-scale-multiple",
      size: 'large',
      bdColor: "rgba(255,255,255,0)",
      color: '#333333',
    })
  }

  idle(){
    this.busyRequestCount--;
    if(this.busyRequestCount<=0){
      this.busyRequestCount =0;
      this.ss.hide();
    }
  }

}
