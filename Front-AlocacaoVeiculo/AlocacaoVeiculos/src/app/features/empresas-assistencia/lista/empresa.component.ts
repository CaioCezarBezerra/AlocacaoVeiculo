import { Component } from '@angular/core';
import { GridEmpresaComponent } from '../form/grid-empresa.component';

@Component({
  selector: 'app-empresa',
  imports: [GridEmpresaComponent],
  templateUrl: './empresa.component.html',
  styleUrl: './empresa.component.scss',
})
export class EmpresaComponent {}
