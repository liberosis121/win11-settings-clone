# UI assets and attribution

Small images support the HW1012 screen-cloning exercise. The interface itself is
built with XAML controls. No asset embeds or crops `References/settings-reference.png`.

## Microsoft product artwork

- `WindowsBloom.jpg` is an unmodified copy of the Windows 11 Bloom wallpaper
  installed at `C:\Windows\Web\Wallpaper\Windows\img0.jpg` on the development
  computer. It is used only inside the small device preview. Copyright belongs
  to Microsoft; this is not an original drawing or an MIT-licensed asset.
- `Microsoft365Apps.svg` arranges ten individual app illustrations. Word, Excel,
  PowerPoint, Outlook, OneNote, OneDrive, Defender and Clipchamp come from the
  Microsoft Office CDN, retrieved on 2026-10-14:
  `https://res.cdn.office.net/midgard/versionless/fluentui-resources/1.1.6/assets/brand-icons/product/svg/{product}_32x1.svg`.
  The eight product names are `word`, `excel`, `powerpoint`, `outlook`, `onenote`,
  `onedrive`, `defender` and `clipchamp`. Only placement, uniform scaling and SVG
  ID prefixes are changed; the artwork paths and gradients are preserved.
  Copilot and Designer remain locally drawn approximations.
- `OneDriveLogo.svg` uses the same Microsoft CDN OneDrive artwork, with transparent
  viewport padding adjusted to fit the reference card.
- `Office-Assets-LICENSE.txt` preserves the notice from
  [Office UI Fabric](https://github.com/OfficeDev/office-ui-fabric-core/blob/master/LICENSE).
  Its MIT notice does not override the separate terms for referenced product
  assets: [Microsoft Fabric asset terms](https://aka.ms/fabric-assets-license).

Microsoft product artwork is used to identify the depicted products in this
educational Settings clone. Microsoft, Windows, Copilot, OneDrive and the app
names are trademarks of their respective owners. No affiliation is asserted and
no ownership or general redistribution license for Microsoft brand artwork is claimed.

## Sidebar icons

`Navigation/Home.svg`, `Network.svg`, `Personalization.svg`, `Apps.svg`,
`Accounts.svg`, `Time.svg` and `Privacy.svg` are derived from 24-pixel color icons
in [Microsoft Fluent UI System Icons](https://github.com/microsoft/fluentui-system-icons),
fixed at revision `a347af95478f989038cccbf085978865264c048a`:

| Local file | Upstream asset folder / icon |
| --- | --- |
| Home | `Home/ic_fluent_home_24_color.svg` |
| Network | `WiFi/ic_fluent_wifi_24_color.svg` |
| Personalization | `Paint Brush/ic_fluent_paint_brush_24_color.svg` |
| Apps | `Apps/ic_fluent_apps_24_color.svg` |
| Accounts | `Person/ic_fluent_person_24_color.svg` |
| Time | `Clock/ic_fluent_clock_24_color.svg` |
| Privacy | `Shield/ic_fluent_shield_24_color.svg` |

The files live under upstream `assets/{folder}/SVG/`. Home, Network, Accounts,
Privacy and Personalization colors are adapted to the reference palette;
Personalization is rotated. The original MIT license and Microsoft copyright
notice are included in `Navigation/LICENSE.txt`.

`Navigation/System.svg`, `Bluetooth.svg`, `Gaming.svg`, `Accessibility.svg` and
`Update.svg` are locally authored vector approximations. Sidebar imagery remains
an approximation of Windows Settings icons, rather than a claim of exact asset parity.

## Other local illustrations

- `MicrosoftLogo.svg`: four colored squares representing the Microsoft mark.
- `Headphones.svg`: shaded headset illustration for the two mock devices.
- `BackupCloud.svg`: cloud upload outline matching the backup action.
- `DeviceWallpaper.svg`: the previous abstract device illustration, retained as
  an unused local fallback; Home now displays `WindowsBloom.jpg`.

Existing application logos and splash images remain Visual Studio template assets.
