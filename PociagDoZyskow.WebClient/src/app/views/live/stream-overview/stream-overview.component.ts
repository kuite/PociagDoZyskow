import { Component, Input, OnInit } from '@angular/core';
import { StreamForm } from '../../../viewmodels/stream/StreamForm';
import { Router } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';


@Component({
  selector: 'stream-overview',
  templateUrl: 'stream-overview.component.html',
  styleUrls: ['./stream-overview.scss']
})



export class StreamOverviewComponent implements OnInit {
  @Input() stream: StreamForm;

  private StreamUrl: SafeResourceUrl;

  constructor(
    private router: Router,
    private sanitizer: DomSanitizer) 
  { 
    
  }

  viewStream(stream) {
    let url = '/live/live/' + stream.id;
    this.router.navigate([url]);
  }

  ngOnInit() 
  {
    this.StreamUrl = this.sanitizer.bypassSecurityTrustResourceUrl(this.stream.url);
    // this.StreamUrl = this.sanitizer.bypassSecurityTrustResourceUrl("https://www.youtube.com/embed/JqPXMISUYYY");
  }
}

