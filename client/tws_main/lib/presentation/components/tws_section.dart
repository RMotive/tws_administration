import 'package:cosmos_foundation/helpers/theme.dart';
import 'package:cosmos_foundation/models/structs/theme_color_struct.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/core/theme/structs/section_theme_struct.dart';
import 'package:tws_main/core/theme/theme_base.dart';

/// TWS Business component.
///
/// Defined to handle section separator along pages sections.
/// Theme Struct:
///   - required[pageColorStruct]
///   - optional[twsSectionStruct]
class TWSSection extends StatelessWidget {
  /// Indicates the display title to show in the section.
  final String title;

  /// The content to be rendered in the section.
  final Widget content;

  /// The padding that the border decorator will use
  final EdgeInsets padding;

  /// Renders a page section.
  ///
  /// Theme Struct:
  ///   - required[pageColorStruct]
  ///   - optional[twsSectionStruct]
  const TWSSection({
    super.key,
    this.padding = const EdgeInsets.all(20),
    required this.title,
    required this.content,
  });

  @override
  Widget build(BuildContext context) {
    final ThemeBase theme = getTheme();
    final ThemeColorStruct pageStruct = theme.pageColorStruct;
    final SectionThemeStruct? sectionStruct = theme.twsSectionStruct;

    return Padding(
      padding: padding,
      child: DecoratedBox(
        decoration: const BoxDecoration(
          border: Border.fromBorderSide(
            BorderSide(
              width: .3,
              color: Colors.white,
            ),
          ),
        ),
        child: SizedBox(
          width: double.maxFinite,
          child: Stack(
            children: <Widget>[
              Padding(
                padding: const EdgeInsets.all(20.0),
                child: Transform.translate(
                  offset: const Offset(0, -35),
                  child: ColoredBox(
                    color: pageStruct.mainColor,
                    child: Padding(
                      padding: const EdgeInsets.all(5.0),
                      child: Text(
                        title,
                        style: sectionStruct?.titleStyle ??
                            TextStyle(
                              color: pageStruct.onColor,
                              fontSize: 18,
                              fontStyle: FontStyle.italic,
                              fontWeight: FontWeight.w600,
                            ),
                      ),
                    ),
                  ),
                ),
              ),
              Padding(
                padding: const EdgeInsets.all(20.0),
                child: content,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
