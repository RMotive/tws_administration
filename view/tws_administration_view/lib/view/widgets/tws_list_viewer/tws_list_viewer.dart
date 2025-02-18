import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/constants/twsa_colors.dart';
import 'package:tws_administration_view/core/theme/bases/twsa_theme_base.dart';
import 'package:tws_administration_view/view/widgets/tws_display_flat.dart';
import 'package:tws_administration_view/view/widgets/tws_section.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';

part 'tws_list_viewer_body.dart';
/// [TwsListViewer] A simple list component to show a section that contains a list with a title and subtitle.
/// Ideal for showing simple data collections.

class TwsListViewer<T> extends StatelessWidget {
  /// Section title.
  final String title;
  /// A list of data objects to print the list.
  /// if  both [agent] and [tilesContent] properties are not null, will cause an assert exception.
  /// Only [agent] value or [tilesContent] is valid.
  final List<T>? tilesContent;
  /// Method to get the title from the [T] type object.
  final String Function(T set) tileTitle;
  /// List heigth.
  final double? heigth;
  /// Items padding
  final EdgeInsetsGeometry padding;
  /// Item background color.
  final Color? backgroundColor;
  /// Item text color.
  final Color? textColor;
  /// Title text alignment.
  final TextAlign titleAlignment;
  /// Text to show when list content is empty.
  final String emptyContentMessage;
  /// Custom header implementation.
  final Widget? customHeader;
  /// Consumer agent for async content.
  /// if  both [agent] and [tilesContent] properties are not null, will cause an assert exception.
  /// Only [agent] value or [tilesContent] is valid.
  final CSMConsumerAgent? agent;
  /// Consume class for async data.
  final Future<SetViewOut<dynamic>> Function()? consume;
  /// Default delay to consumer.
  final Duration delay;
  
  const TwsListViewer({
    super.key,
    required this.title,
    required this.tileTitle,
    this.tilesContent,
    this.heigth,
    this.customHeader,
    this.emptyContentMessage = "Empty content",
    this.padding = const EdgeInsets.all(5),
    this.textColor,
    this.backgroundColor,
    this.titleAlignment = TextAlign.left,
    this.agent,
    this.consume,
    this.delay = Duration.zero,
  })  : assert((agent == null || consume == null) || tilesContent == null,
            "[Agent] or [consume] property can't be declared when [tilesContent] is not null."),
        assert(((agent != null && consume != null) && tilesContent == null) || ((agent == null && consume == null) && tilesContent != null),
            "if [tilesContent] property is null, then both; [agent] and [consume] properties must be declared.");

  @override
  Widget build(BuildContext context) {
    CSMColorThemeOptions pageColorTheme = getTheme<TWSAThemeBase>().page;
    Color tColor = textColor  ?? pageColorTheme.fore;
    Color bColor = backgroundColor ?? pageColorTheme.main;
    return SizedBox(
      child: TWSSection(
        title: title, 
        content: 
        tilesContent != null?
        _TwsListViewerBody<T>(
          content: tilesContent!, 
          tColor: tColor, 
          bColor: bColor, 
          heigth: heigth, 
          titleAlignment: titleAlignment, 
          emptyContentMessage: emptyContentMessage, 
          padding: padding, 
          title: title, 
          tileTitle: tileTitle
        ): 
        CSMConsumer<SetViewOut<dynamic>>(
          consume: consume!,
          agent: agent,
          delay: delay,
          loadingBuilder: (BuildContext ctx) {
            return Center(
              child: CircularProgressIndicator(
                color: pageColorTheme.fore,
              ),
            );
          },
          successBuilder:(BuildContext ctx, SetViewOut<dynamic> data) {
            return _TwsListViewerBody<T>(
              content: data.sets as List<T>, 
              tColor: tColor, 
              bColor: bColor, 
              heigth: heigth, 
              titleAlignment: titleAlignment, 
              emptyContentMessage: emptyContentMessage, 
              padding: padding, 
              title: title, 
              tileTitle: tileTitle,
            );
          },  
        ),
      ),
    );
  }
}