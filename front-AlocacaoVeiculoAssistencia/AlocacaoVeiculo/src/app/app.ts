import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { veiculoComponent } from "./pages/veiculo/veiculo.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, veiculoComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('AlocacaoVeiculo');
}
