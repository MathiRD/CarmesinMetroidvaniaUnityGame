# Metroidvania MVP — Projeto Unity

Um **metroidvania 2D** com movimento fluido, **pulo**, **wall slide/grab**, **dash com desbloqueio por pickup**, combate básico, UI e áudio/SFX. Este repositório está organizado para rodar diretamente no Unity e servir como base para evolução do projeto.

---

## 🎮 Controles

| Ação | Tecla |
|---|---|
| Mover | **Setas** (← →) |
| Pular | **Z** |
| Atacar | **X** |
| Dash *(quando desbloqueado)* | **C** |

> ⚠️ O dash começa **bloqueado** e é liberado ao coletar o **pickup de dash** presente no mapa.

---

## 🚀 Como rodar

1. **Abra no Unity**  
   - Use a **mesma versão do projeto** (verifique em `ProjectSettings/ProjectVersion.txt`).  
   - *Recomendado:* versão idêntica à que gerou o ZIP.

2. **Importe/valide pacotes** (Package Manager)  
   - `Input System`, `Cinemachine`, `TextMeshPro`, `Addressables`, *(e outros do manifest)*.

3. **DOTween (Demigiant)**  
   - Menu: **Tools → Demigiant → DOTween Utility Panel → Setup DOTween**.  
   - Aguarde a geração das DLLs/definições.

4. **Addressables** (se usados na sua build)  
   - Menu: **Window → Asset Management → Addressables → Groups**.  
   - Clique **Build → New Build → Default Build Script**.

5. **Input System**  
   - **Edit → Project Settings → Player → Active Input Handling = Input System**.  
   - Se houver um `.inputactions`, selecione-o e clique **Generate C# Class**.

6. **Cena inicial**  
   - Abra `Assets/Scenes/Samples/ThePaleMoonlight_Sample.unity`.  
   - **File → Build Settings** → **Add Open Scenes** e deixe essa cena **no topo**.

7. **Áudio**  
   - Se o projeto avisar de **AudioMixer** ausente em `GameAudioSettings`, atribua o mixer do projeto no asset correspondente (ou use o auto‑assign, se incluído).

8. **Executar**  
   - **Play** no Editor para testar.  
   - **File → Build & Run** para gerar a build.

---

## 🔁 Resetar progresso / travar o dash novamente

- **Método rápido (PlayerPrefs):**
  - No Editor: **Window → Analysis →** *ou* um menu de utilitário disponível no projeto (`Tools/Metroidvania/Clear All PlayerPrefs`, se existir).  
  - Ou via código (C#), em um script temporário:
    ```csharp
    UnityEngine.PlayerPrefs.DeleteKey("mv_has_dash");
    UnityEngine.PlayerPrefs.Save();
    ```
- Reinicie o jogo: o dash voltará a iniciar **bloqueado** até pegar o pickup.

---

## 🧩 Estrutura (resumo)

- `Assets/Scripts/Modules/`  
  - **Abilities**: Dash, pulo, etc.  
  - **Characters**: Player e base de personagens.  
  - **Enemies**: Inimigos e IA básica.  
  - **Input**: Integração com o Input System.  
  - **SceneManagement**: Loader, transições e canais.  
  - **Serialization**: `GameData`, `DataManager` (salvamento/carregamento).  
  - **Settings**: `GameAudioSettings`, `GameLocalizationSettings`.  
  - **Utility**: Helpers, gizmos, extensões.
- `Assets/Scenes/`  
  - **Samples/ThePaleMoonlight_Sample.unity** *(principal)*  
  - Outras cenas de exemplo (menu, game over, etc.).
- `Assets/Audio/`, `Assets/Animations/`, `Assets/Sprites/`, `Assets/Materials/`, `Assets/UI/`

---

## 👤 Autor & Créditos

**Matheus Durigon Rodrigues** — *Desenvolvimento lógico, implementação de mecânicas (movimento, pulo, wall slide/grab, dash com gating, combate), montagem de cena teste*

**Maria Eduarda Carvalho Dornelles** — *Implementação do mapa utilizando assets prontos, incluindo o pack **Platformer Tileset – PixelArt Grasslands**, além de ajustes visuais e integração de imagens adicionadas ao projeto.*

### Terceiros / Middleware
- **DOTween (Demigiant)** — tweening/anim. auxiliares.  
- **Cinemachine** — câmera virtual.  
- **TextMesh Pro** — UI/Texto.  
- **Addressables** — gerenciamento de assets (quando aplicável).  
- Outros assets de arte/áudio/créditos conforme licenças originais.

> Se você reutilizar este projeto, preserve os créditos e as licenças dos assets de terceiros.

---

## 🛠️ Solução de problemas (FAQ)

- **Erros de `DG.Tweening.*` (DOTween)**  
  - Execute **Setup DOTween** (passo 3). Verifique **Api Compatibility Level = .NET 4.x** (Project Settings → Player).

- **`MissingReferenceException: AudioMixer`**  
  - Abra o asset `GameAudioSettings` e atribua um **AudioMixer** válido (ou mantenha o auto‑assign, se o projeto incluir).

- **`NullReference` em SafePoints**  
  - Garanta que o **Player** tem Tag **`Player`** e que colliders de checkpoints estão com **IsTrigger**.

- **Cena errada ao rodar**  
  - Em **Build Settings**, a cena correta precisa estar **no topo**. Abra a cena e clique **Add Open Scenes**.

---

## 📄 Licença

Este repositório é fornecido como **MVP educacional/demonstrativo**.  
Verifique e respeite as licenças dos **assets de terceiros** incluídos.
