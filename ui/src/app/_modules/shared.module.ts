import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ToastrModule } from 'ngx-toastr';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-botton-right',
    }),
    TabsModule.forRoot(),
    NgxGalleryModule,
    FontAwesomeModule
  ],
  exports: [
    BsDropdownModule,
    ToastrModule, 
    TabsModule,
    NgxGalleryModule,
    FontAwesomeModule
  ]
})
export class SharedModule { }
