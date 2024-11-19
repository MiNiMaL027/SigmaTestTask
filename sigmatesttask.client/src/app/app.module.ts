import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HttpService } from 'src/services/httpClientService';
import { FileUploadComponent } from 'src/views/file-upload/file-upload.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';

const appRoutes: Routes =[
  { path: '', redirectTo:"/file-upload", pathMatch:"full"},
  {path:'/file-upload', component: FileUploadComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    FileUploadComponent
  ],
  imports: [BrowserModule, FormsModule, HttpClientModule,RouterModule.forRoot(appRoutes)],
  providers: [HttpService],
  bootstrap: [AppComponent]
})
export class AppModule { }
