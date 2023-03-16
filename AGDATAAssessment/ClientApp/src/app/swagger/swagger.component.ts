import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

declare const SwaggerUIBundle : any;

@Component({
  selector: 'app-swagger',
  templateUrl: './swagger.component.html',
  styleUrls: ['./swagger.component.css']
})
export class SwaggerComponent implements OnInit {

  @ViewChild('swagger') swaggerDom : ElementRef<HTMLDivElement>;

  ngOnInit () {
    const ui = SwaggerUIBundle({
      dom_id: '#swagger-ui',
      layout: 'BaseLayout',
      presets: [
        SwaggerUIBundle.presets.apis,
        SwaggerUIBundle.SwaggerUIStandalonePreset
      ],
      url: '/swagger/v1/swagger.json',
      docExpansion: 'none',
      operationsSorter: 'alpha'
    });
  }

}
