import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';


export interface GrupoVeiculos {
    id: number;
    nome: string;
    descricao: string
}


export interface ListarGrupoVeiculo {
    id: number;
    nome: string;
    descricao: string;
}


export interface CriarGrupoVeiculo {
    id: number;
    nome: string;
    descricao: string;
}


export interface ExcluirGrupoVeiculo {
    id: number;
    nome: string;
    descricao: string;
}






@Service()
export class GrupoVeiculoService {
    private readonly http = inject(HttpClient)

    private readonly apiUrl = 'http://localhost:7005/api/GruposVeiculos'

    ListarGrupoVeiculos(): Observable<ListarGrupoVeiculo[]> {
        return this.http.get<GrupoVeiculos[]>(`${this.apiUrl}/ListarGruposVeiculos`);

    }


    CriarGruposVeiculos(grupoVeiculos: CriarGrupoVeiculo) {
        return this.http.post<CriarGrupoVeiculo>(`${this.apiUrl}/CriarGruposVeiculos`, grupoVeiculos)
    }

    AtualizarGrupo(id: number, grupo: GrupoVeiculos): Observable<GrupoVeiculos> {
        return this.http.put<GrupoVeiculos>(`${this.apiUrl}/AtualizarGrupoVeiculos/${id}`, grupo);
    }
    deletar(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/DeletarGrupoVeiculos/${id}`);
    }

}
