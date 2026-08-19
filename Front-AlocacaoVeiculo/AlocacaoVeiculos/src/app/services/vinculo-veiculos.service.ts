import { HttpClient } from '@angular/common/http';
import { inject, Injectable, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { CriarVinculos } from '../features/vinculos/form/grid-vinculo-veiculos.component';


export interface VinculoVeiculo {
    id: number;
    veiculoId: number;
    veiculo: string;
    planoId: number;
    plano: string;
}


export interface CriarVinculo {
    veiculoId: number;
    planoId: number;

}

export interface ListarVinculo {
    id: number;
    veiculoId: number;
    veiculo: string;
    planoId: number;
    plano: string;

}

export interface AtualizaVinculo {
    id: number;
    veiculo: string;
    plano: string;

}

export interface ExcluitVinculo {
    id: number;
    veiculoId: number;
    veiculo: string;
    planoId: number;
    plano: string;

}

export interface Vinculo {
    id: number;
    veiculo: string;
    plano: string;

}

@Service()
export class VinculoVeiculo {


    private readonly http = inject(HttpClient);

    private readonly apiUrl = 'http://localhost:7005/api/VeiculosAssistencias';
    /*----------------------------------LISTAR----------------------------------------------------------*/
    listarVinculos(): Observable<VinculoVeiculo[]> {
        return this.http.get<VinculoVeiculo[]>(`${this.apiUrl}/ListarVinculoVeiculo`);
    }
    /*----------------------------------ATUALIZAR----------------------------------------------------------*/
    atualizar(id: number, veiculo: AtualizaVinculo): Observable<AtualizaVinculo> {
        return this.http.put<AtualizaVinculo>(`${this.apiUrl}/AtualizarVinculoVeiculo/${id}`, veiculo);
    }
    /*----------------------------------DELETAR----------------------------------------------------------*/
    deletar(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/DeletarVinculoVeiculo/${id}`);

    }
    criarVinculo(dados: CriarVinculo): Observable<Vinculo> {
        return this.http.post<Vinculo>(
            `${this.apiUrl}/CriarVinculos`,
            dados
        );
    }



}
