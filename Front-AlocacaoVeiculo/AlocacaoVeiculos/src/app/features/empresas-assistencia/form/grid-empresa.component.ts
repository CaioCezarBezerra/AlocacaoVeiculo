import { Component } from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { EmpresaAssistencia } from '../../../services/empresa-assistencia.service';


export interface PeriodicElement {
  nome: string;
  position: number;
  endereco: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, nome: 'Hydrogen', endereco: "1.0079"},
  {position: 2, nome: 'Helium', endereco: "1.0079"},
  {position: 3, nome: 'Lithium', endereco: "1.0079"},
  {position: 4, nome: 'Beryllium', endereco: "1.0079"},
  {position: 5, nome: 'Boron', endereco: "1.0079"}
];


@Component({
  selector: 'app-grid-empresa',
  imports: [MatExpansionModule, MatFormFieldModule, MatInputModule, MatTableModule, MatIconModule],
  templateUrl: './grid-empresa.component.html',
  styleUrl: './grid-empresa.component.scss',
})
export class GridEmpresaComponent {


expanded = false;

novaEmpresa(event: MouseEvent): void {
  event.stopPropagation();

}
  
  displayedColumns: string[] = ['position', 'nome', 'endereco'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
