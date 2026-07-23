# 🐾 Diário de Adestramento

API em .NET para registrar sessões de adestramento canino — o que foi treinado, quanto tempo durou, onde aconteceu e qual era o clima no início e no fim do treino (buscado automaticamente).

> Projeto pessoal em desenvolvimento, feito pra aprender e aplicar boas práticas de arquitetura em .NET na prática, com um caso de uso real.

## Por que um diário de adestramento

A inspiração pra esse projeto veio de um manual de adestramento de cães de faro do Exército — que não é, nem de longe, o contexto aqui (o "cão de guerra" deu lugar ao seu cachorro de casa, treinando "senta" e "fica" em vez de detectar entorpecentes). Mas um princípio desse material se aplicou muito bem: **um adestramento que não é registrado com atenção não pode ser melhorado**.

O manual descreve que o condutor mantém um diário detalhado — o que foi trabalhado, onde, com que dificuldade o cão respondeu, o que atrapalhou a sessão. Não é burocracia por burocracia: é o que permite, depois, olhar pra trás e identificar padrões. Trazendo isso pro nosso caso:

- **Registrar com detalhe, não só "treinei hoje"**: o quê foi treinado, quanto tempo durou, que recompensa foi usada, como o cão respondeu (`Excelente`, `Bom`, `Demorado`) e observações do que aconteceu na sessão.
- **Dificuldades fazem parte do registro, não são motivo de constrangimento**: se o cão vai bem em "senta" mas trava toda vez em "fica", isso precisa aparecer no diário — é justamente aí que o adestramento precisa de mais atenção, não é algo pra esconder ou ignorar.
- **Estatística revela o que a memória não guarda**: o manual dá um exemplo direto — se um cão acerta 95% com uma substância e só 5% com outra, tem algo no método que precisa mudar. Da mesma forma, se as sessões registradas mostrarem que o tempo de resposta piora sistematicamente em determinada condição de clima, ou num determinado local, ou com um certo tipo de comando, isso é sinal de onde focar o esforço.
- **100% de acerto também é um dado importante**: o manual observa que um cão acertando sempre pode significar que o exercício parou de desafiar o suficiente, não que o treino está perfeito. Vale o mesmo aqui: se um comando está sempre "Excelente", talvez seja hora de aumentar a dificuldade (mais distração, ambiente novo, comando mais longo) em vez de repetir o que já está dominado.

É por isso que o projeto guarda o clima de cada sessão automaticamente, e é por isso que a ideia de gerar estatísticas (ver [Roadmap](#roadmap)) não é um extra decorativo — é o próprio motivo de existir um diário estruturado em banco de dados, em vez de um caderno: permitir, mais pra frente, cruzar esses dados e enxergar padrões que a memória sozinha não conseguiria perceber sessão após sessão.

## O que o projeto faz

- Cadastra cachorros e locais de treino (com latitude/longitude)
- Registra sessões de treino: data, horário de início/fim, o que foi treinado, recompensa usada e tempo de resposta do cão
- Busca automaticamente o clima histórico (temperatura, condição, vento, precipitação) do momento exato do início e do fim de cada sessão, via [Open-Meteo](https://open-meteo.com/)
- Lista sessões de um cachorro específico, com paginação

## Stack técnica

- **.NET** (ASP.NET Core Web API)
- **Entity Framework Core** + **SQLite**
- **Open-Meteo Historical Weather API** para dados de clima históricos
- Documentação interativa via **Swagger**



## Como rodar localmente

### Pré-requisitos
- .NET SDK instalado
- (Opcional) Uma IDE como Visual Studio ou VS Code

### Passos

```bash
git clone https://github.com/DanieelCarvalho/DiarioAdestramento.git
cd DiarioAdestramento
```

Aplicar as migrations e criar o banco local (SQLite):

```bash
dotnet ef database update
```

Rodar a aplicação:

```bash
dotnet run
```

A documentação interativa (Swagger) fica disponível em `/swagger` assim que o projeto sobe, em ambiente de desenvolvimento.

## Endpoints principais

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/cachorro` | Lista cachorros (paginado) |
| `GET` | `/api/cachorro/{id}` | Busca um cachorro específico |
| `GET` | `/api/cachorro/{id}/sessoes` | Sessões de um cachorro, com dados de clima e local (paginado) |
| `POST` | `/api/cachorro` | Cadastra um cachorro |
| `GET` | `/api/local` | Lista locais de treino |
| `POST` | `/api/local` | Cadastra um local (com latitude/longitude) |
| `GET` | `/api/sessaotreino/{id}` | Detalhes completos de uma sessão (clima incluso) |
| `POST` | `/api/sessaotreino` | Cria uma sessão — o clima do início e do fim é buscado automaticamente |
| `PUT` | `/api/sessaotreino/{id}` | Atualiza campos de texto de uma sessão (não altera horário/local) |
| `DELETE` | `/api/sessaotreino/{id}` | Remove uma sessão |

## Roadmap

- [ ] Autenticação
- [ ] Front-end (ASP.NET Core MVC)
- [ ] Estatísticas: relação entre condição de clima e tempo de resposta do cão
- [ ] Aplicativo (.NET MAUI), com câmera para fotos/vídeos das sessões

---

Projeto pessoal, em desenvolvimento contínuo — este README será atualizado conforme novas features forem adicionadas.
