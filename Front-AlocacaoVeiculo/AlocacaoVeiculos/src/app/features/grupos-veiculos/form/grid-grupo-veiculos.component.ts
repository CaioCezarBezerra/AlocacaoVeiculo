import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CriarGrupoVeiculo, ExcluirGrupoVeiculo, GrupoVeiculos, GrupoVeiculoService } from '../../../services/grupo-veiculo.service';

@Component({
  selector: 'app-grupos-veiculos',
  imports: [
    ReactiveFormsModule,
    MatTooltipModule,
    MatIconModule,
    MatExpansionModule,
    MatTableModule,
    MatInputModule,
    MatButtonModule,
    MatStepperModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule
  ],
  templateUrl: './grid-grupo-veiculos.component.html',
  styleUrl: './grid-grupo-veiculos.component.scss',
})
export class GridGrupoVeiculosComponent {


  private readonly formBuilder = inject(FormBuilder);
  private readonly grupoVeiculosService = inject(GrupoVeiculoService);

  grupoVeiculos: GrupoVeiculos[] = []


  dataSource = new MatTableDataSource<GrupoVeiculos>([]);

  editando: GrupoVeiculos | null = null;

  @ViewChild('nomeInput')
  nomeInput!: ElementRef<HTMLInputElement>;

  @ViewChild('stepper')
  stepper!: MatStepper;


  ngOnInit() {
    this.listarGrupoVeiculos();
  }
  displayedColumns: string[] = [
    'id',
    'nome',
    'descricao',
    'acoes'
  ];

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

  nomeFormGroup =
    this.formBuilder.group({

      nome: [
        '',
        Validators.required
      ]

    });


  descricaoFormGroup =
    this.formBuilder.group({

      descricao: [
        '',
        Validators.required
      ]

    });


  /*----------------------------------CRIAR----------------------------------------------------------*/
  cadastrarGrupo(): void {

    if (
      this.nomeFormGroup.invalid ||
      this.descricaoFormGroup.invalid) {
      return;
    }

    const dados: CriarGrupoVeiculo = {
      id: this.idFormGroup.controls.id.value!,
      nome: this.nomeFormGroup.controls.nome.value!,
      descricao: this.descricaoFormGroup.controls.descricao.value!,
    };

    this.grupoVeiculosService.CriarGruposVeiculos(dados).subscribe({
      next: (resultado) => {
        console.log('Grupo criado:', resultado);
        this.listarGrupoVeiculos();


        this.nomeFormGroup.reset();
        this.descricaoFormGroup.reset();

        this.stepper.reset();

        setTimeout(() => {
          this.nomeInput.nativeElement.focus();
        });
      },

      error: (erro) => {
        console.error('Erro ao criar grupo:', erro);
      }
    });



    const novoPlano = {

      nomes:
        this.nomeFormGroup.controls.nome.value,
      id:
        this.idFormGroup.controls.id.value,
      descricao:
        this.descricaoFormGroup.controls.descricao.value

    };


    console.log(
      'NOVO PLANO:',
      novoPlano
    );

  }

  /*----------------------------------LISTAR----------------------------------------------------------*/
  listarGrupoVeiculos() {
    this.grupoVeiculosService.ListarGrupoVeiculos().subscribe({
      next: (plano) => {
        console.log("Ola")
        this.dataSource.data = plano;
      },
      error: (erro) => {
        console.error('Erro ao buscar veículos:', erro);
      }
    });
  }

  editar(grupo: GrupoVeiculos) {
    this.editando = grupo;
  }

  /*----------------------------------SALVAR----------------------------------------------------------*/
  salvar(grupo: GrupoVeiculos): void {

    const dados = {
      nome: grupo.nome,
      descricao: grupo.descricao,
      id: grupo.id
    };

    console.log('Dados alterados:', dados);

    this.grupoVeiculosService.AtualizarGrupo(grupo.id, dados)
      .subscribe({

        next: (resultado) => {

          console.log(
            'Grupo atualizado:',
            resultado
          );

          this.listarGrupoVeiculos();
        },

        error: (erro) => {

          console.error(
            'Erro ao atualizar plano:',
            erro
          );

        }

      });
  }
  /*----------------------------------EXCLUIR----------------------------------------------------------*/
  excluir(grupo: ExcluirGrupoVeiculo) {
    this.grupoVeiculosService.deletar(grupo.id).subscribe({
      next: () => {
        console.log('Grupo excluído');
        this.listarGrupoVeiculos();
      },
      error: (erro) => {
        console.error('Erro ao excluir:', erro);
      }
    });
  }

}

