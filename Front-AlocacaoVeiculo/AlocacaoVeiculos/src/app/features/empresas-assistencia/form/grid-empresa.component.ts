import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { CriaEmpresa, EmpresaAssistencia, EmpresaAssistenciaService, ExcluirEmpresa } from '../../../services/empresa-assistencia.service';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';


export interface PeriodicElement {
  nome: string;
  Id: number;
  endereco: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { Id: 1, nome: 'Hydrogen', endereco: "1.0079" },
  { Id: 2, nome: 'Helium', endereco: "1.0079" },
  { Id: 3, nome: 'Lithium', endereco: "1.0079" },
  { Id: 4, nome: 'Beryllium', endereco: "1.0079" },
  { Id: 5, nome: 'Boron', endereco: "1.0079" }
];


@Component({
  selector: 'app-grid-empresa',
  imports: [
    MatExpansionModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatIconModule,
    MatStepperModule,
    ReactiveFormsModule,
    FormsModule


  ],
  templateUrl: './grid-empresa.component.html',
  styleUrl: './grid-empresa.component.scss',
})
export class GridEmpresaComponent {

  editando: EmpresaAssistencia | null = null;

  ngOnInit() {
    this.listarEmpresa();
  }

  private readonly formBuilder = inject(FormBuilder);
  private readonly empresaService = inject(EmpresaAssistenciaService)
  expanded = false;
  @ViewChild('nomeInput')
  nomeInput!: ElementRef<HTMLInputElement>;

  @ViewChild('stepper')
  stepper!: MatStepper;

  novaEmpresa(event: MouseEvent): void {
    console.log("ola")
    event.stopPropagation();

  }

  displayedColumns: string[] = ['id', 'nome', 'endereco', 'acoes'];

  dataSource = new MatTableDataSource<EmpresaAssistencia>([]);

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }


  nomeFormGroup =
    this.formBuilder.group({

      nome: [
        '',
        Validators.required
      ]


    });
  enderecoFormGroup =
    this.formBuilder.group({

      endereco: [
        '',
        Validators.required
      ]


    });

  idFormGroup =
    this.formBuilder.group({

      id: [
        null as number | null,
        [
          Validators.required,
          Validators.min(1)
        ]
      ]

    });

  /*----------------------------------CADASTRA EMPRESA----------------------------------------------------------*/
  cadastrarEmpresa(): void {

    if (
      this.nomeFormGroup.invalid ||
      this.enderecoFormGroup.invalid) {
      return;
    }

    const dados: CriaEmpresa = {
      nome: this.nomeFormGroup.controls.nome.value!,
      endereco: this.enderecoFormGroup.controls.endereco.value!
    };

    this.empresaService.criarEmpresa(dados).subscribe({
      next: (resultado) => {

        this.listarEmpresa();


        this.nomeFormGroup.reset();
        this.enderecoFormGroup.reset();

        this.stepper.reset();

        setTimeout(() => {
          this.nomeInput.nativeElement.focus();
        });
      },

      error: (erro) => {
        console.error('Erro ao criar plano:', erro);
      }
    });

    const novoEmpresa = {

      nomeEmpresa:
        this.nomeFormGroup.controls.nome.value,

      endereco:
        this.enderecoFormGroup.controls.endereco.value,
      id:
        this.idFormGroup.controls.id.value

    };


    console.log(
      'NOVA EMPRESA:',
      novoEmpresa
    );

  }

  editar(plano: EmpresaAssistencia) {
    this.editando = plano;
  }

  /*----------------------------------LISTAR----------------------------------------------------------*/
  listarEmpresa() {
    this.empresaService.listarEmpresa().subscribe({
      next: (empresa) => {
        console.log("Ola")
        this.dataSource.data = empresa;
      },
      error: (erro) => {
        console.error('Erro ao buscar veículos:', erro);
      }
    });
  }
  /*----------------------------------EXCLUIR----------------------------------------------------------*/
  excluir(empresa: ExcluirEmpresa) {
    this.empresaService.deletar(empresa.id).subscribe({
      next: () => {
        console.log('empresa excluído');
        this.listarEmpresa();
      },
      error: (erro) => {
        console.error('Erro ao excluir:', erro);
      }
    });
  }
  /*----------------------------------SALVAR----------------------------------------------------------*/
  salvar(empresa: EmpresaAssistencia): void {

    const dados = {
      id: empresa.id,
      nome: empresa.nome,
      endereco: empresa.endereco,
    };

    this.empresaService.atualizar(empresa.id, dados)
      .subscribe({

        next: (resultado) => {

          console.log(
            'Empresa atualizado:',
            resultado
          );

          this.listarEmpresa();
        },

        error: (erro) => {

          console.error(
            'Erro ao atualizar empresa',
            erro
          );

        }

      });
  }
  /*---------------------------------------------------------------------------------------------------*/

}
