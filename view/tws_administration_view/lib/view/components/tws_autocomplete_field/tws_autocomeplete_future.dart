part of 'tws_autocomplete_field.dart';

class _TWSAutocompleteFuture<T> extends StatelessWidget {
  final Future<List<SetViewOut<dynamic>>> Function() consume;
  final ScrollController controller;
  final List<T> suggestions;
  final double tileHeigth;
  final CSMColorThemeOptions theme;
  final String Function(T?) displayLabel;
  final void Function(String label) onTap;
  final List<T> Function(List<SetViewOut<dynamic>> data) onFirstBuild;
  final Color loadingColor;
  final Color hoverTextColor;
  final bool firstBuild;

  const _TWSAutocompleteFuture({
    required this.consume,
    required this.controller,
    required this.suggestions,
    required this.displayLabel,
    required this.theme,
    required this.onTap,
    required this.loadingColor,
    required this.hoverTextColor,
    required this.onFirstBuild,
    required this.tileHeigth,
    required this.firstBuild
  });

  @override
  Widget build(BuildContext context) {
    return CSMConsumer<List<SetViewOut<dynamic>>>(
      consume: consume,
        emptyCheck: (List<SetViewOut<dynamic>> data) {
        return false;
      },
      loadingBuilder: (_) {
        return Padding(
          padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 30),
          child: CircularProgressIndicator(
            backgroundColor: TWSAColors.darkGrey,
            color: loadingColor,
            strokeWidth: 4,
          ),
        );
      },
        errorBuilder: (BuildContext ctx, Object? error, List<SetViewOut<dynamic>>? data) {
        return const Padding(
          padding: EdgeInsets.all(10),
          child: TWSDisplayFlat(
            display: 'Error loading data',
          ),
        );
      },
        successBuilder: (BuildContext ctx, List<SetViewOut<dynamic>> rawData) {  
        return suggestions.isNotEmpty || firstBuild? Scrollbar(
          trackVisibility: true,
          thumbVisibility: true,
          controller: controller,
          child: _TWSAutocompleteList<T>(
            controller: controller,
            list: firstBuild? onFirstBuild(rawData) : suggestions,
            displayLabel: displayLabel,
            theme: theme,
            hoverTextColor: hoverTextColor,
            onTap: onTap,
          )
        ) 
        :_TWSAutocompleteNotfound(
          height: tileHeigth, 
          color: loadingColor
        );
      }
    );
  }
}