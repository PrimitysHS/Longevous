# 🎧 SDD – Sound Design Document  
**Projeto:** Longevus

---

## 1. Finalidade do documento
Este documento apresenta a estrutura do design sonoro do jogo **Longevus**, descrevendo a utilização de trilhas musicais e efeitos sonoros, seus contextos de aplicação, eventos de acionamento e diretrizes para evolução futura do sistema de áudio.

---

## 2. Caracterização da proposta sonora
A proposta sonora está alinhada à identidade estética e mecânica do jogo:

- Reforçar a ambientação **dark fantasy 2D em pixel art**
- Intensificar a atmosfera sombria e mística do universo
- Comunicar ações de combate e movimentação do jogador
- Destacar momentos narrativos e encontros importantes
- Auxiliar na imersão durante exploração e batalhas

---

## 3. Propósitos do sistema de áudio

- Manter ambientação sonora coerente com o universo dark fantasy
- Reforçar feedback de ações do jogador
- Comunicar eventos críticos e progressão narrativa
- Diferenciar estados de exploração, combate e chefes
- Permitir expansão futura do sistema de áudio

---

## 4. Sistema de Áudio

### 4.1 Trilhas musicais

| Arquivo | Tipo | Aplicação |
|---------|------|-----------|
| `/sound/music/musica_introdutoria.mp3` | Trilha de ambientação | Tocada quando a Morte chega à aldeia dos ocultistas |
| `/sound/music/musica_batalha_acelerada.mp3` | Trilha de combate | Tocada durante batalhas comuns contra inimigos |
| `/sound/music/musica_tensa.mp3` | Trilha de boss | Tocada no encontro com o líder do culto (boss final) |

---

### 4.2 Efeitos sonoros

| Arquivo | Tipo | Aplicação |
|---------|------|-----------|
| `/sound/sfx/movimento_player.mp3` | SFX de movimentação | Som contínuo de flutuação/deslocamento da Morte |
| `/sound/sfx/player_cai_chao.mp3` | SFX de impacto | Quando o jogador pousa após queda/pulo |
| `/sound/sfx/pulo_ataque_player.mp3` | SFX de ação | Executado ao atacar ou durante ação de pulo/ataque |

---

## 5. Função dos componentes sonoros

- As trilhas musicais representam os diferentes estados emocionais da jornada
- Os efeitos sonoros reforçam fisicalidade e impacto das ações do personagem
- A ambientação sonora contribui para caracterizar o mundo e seus perigos
- Os sons auxiliam na leitura de timing e feedback de combate

---

## 6. Eventos de execução sonora

- **Entrada na aldeia:** reprodução da música introdutória
- **Exploração:** ambientação base/silêncio atmosférico (caso implementado futuramente)
- **Combate comum:** transição para música de batalha
- **Encontro com boss:** transição para música tensa
- **Movimentação:** som contínuo de flutuação do player
- **Ataque:** efeito sonoro de golpe
- **Pouso após salto/queda:** efeito de impacto no chão

---

## 7. Sequência sonora da experiência

- Introdução / chegada à aldeia
- Exploração inicial
- Primeiros confrontos com ocultistas
- Progressão por áreas do mapa
- Intensificação sonora conforme avanço
- Encontro com o líder do culto
- Batalha final
- Encerramento (a implementar)

---

## 8. Contribuição do áudio

- Reforça a identidade sobrenatural da protagonista (Morte)
- Aumenta a imersão no universo dark fantasy
- Melhora percepção de impacto das mecânicas de combate
- Ajuda a estabelecer ritmo e tensão narrativa
- Diferencia momentos de exploração e confronto

---

## 9. Parâmetros de mixagem

- Priorizar clareza de efeitos de combate sobre trilha musical
- Manter volume moderado no som de movimentação contínua para evitar repetição cansativa
- Aumentar destaque das trilhas de boss para reforço dramático
- Evitar sobreposição excessiva entre SFX de combate e música

---

## 10. Padronização estética

- Sons devem manter coerência com ambientação mística e sombria
- Efeitos devem possuir textura etérea/sobrenatural para representar a Morte
- Trilha sonora deve reforçar progressão dramática do jogo
- Feedbacks auditivos devem ser limpos e perceptíveis mesmo em combate intenso

---

## 11. Restrições

- Biblioteca sonora inicial reduzida
- Ausência atual de efeitos para inimigos, interface e ambiente
- Sem implementação de áudio espacial/posicional
- Sistema de mixagem ainda simplificado

---

## 12. Melhorias futuras

- Adicionar efeitos sonoros para inimigos e chefes
- Implementar ambientação sonora por bioma/área
- Criar transições dinâmicas entre exploração e combate
- Adicionar efeitos para interface e menus
- Implementar controle de volume para usuário
- Adicionar variações de som para ataques e movimentação

---

## 13. Considerações finais

Os áudios atualmente selecionados foram obtidos a partir de vídeos do YouTube identificados como sem copyright, destinados ao uso preliminar/prototipagem do projeto. Recomenda-se futura substituição ou validação de licenciamento para publicação comercial.

O design sonoro atual atende aos requisitos inicais de ambientação e feedback do jogo, estabelecendo uma base sólida para futuras expansões e refinamentos.