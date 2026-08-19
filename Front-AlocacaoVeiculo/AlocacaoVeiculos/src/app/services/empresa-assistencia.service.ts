import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';



export interface EmpresaAssistencia {
    id: number;
    nome: string;
    endereco: string;
}

export interface CriaEmpresa {
    nome: string;
    endereco: string;
}


export interface ListarEmpresa {
    id: number;
    nome: string;
    endereco: string;
}

export interface ExcluirEmpresa {
    id: number;
    nome: string;
    endereco: string;
}

export interface AtualizarEmpresa {
    nome: string;
    endereco: string;
}


@Service()
export class EmpresaAssistenciaService {

    private readonly http = inject(HttpClient)

    private readonly apiUrl = 'http://localhost:7005/api/EmpresasAssistencia'


    /*----------------------------------CRIAR----------------------------------------------------------*/

    criarEmpresa(empresa: CriaEmpresa): Observable<CriaEmpresa> {
        return this.http.post<CriaEmpresa>(`${this.apiUrl}/CriarEmpresa`, empresa)
    }

    /*----------------------------------LISTAR----------------------------------------------------------*/

    listarEmpresa(): Observable<EmpresaAssistencia[]> {
        console.log("passei aqui empresa")
        return this.http.get<EmpresaAssistencia[]>(
            `${this.apiUrl}/ListarEmpresa`);
    }

    /*----------------------------------DELETAR----------------------------------------------------------*/
    deletar(id: number): Observable<void> {
        return this.http.delete<void>(
            `${this.apiUrl}/DeletarEmpresa/${id}`);
    }
    /*----------------------------------ATUALIZAR----------------------------------------------------------*/
    atualizar(id: number, empresa: EmpresaAssistencia): Observable<AtualizarEmpresa> {
        return this.http.put<AtualizarEmpresa>(`${this.apiUrl}/AtualizarEmpresa/${id}`, empresa);
    }

}
