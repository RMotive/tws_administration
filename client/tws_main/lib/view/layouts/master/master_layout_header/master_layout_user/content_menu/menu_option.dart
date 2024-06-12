part of '../../../master_layout.dart';

class _MenuOption extends StatelessWidget {
  final _Options options;
  final Color color;
  final void Function() onTap;

  const _MenuOption({
    required this.options, 
    required this.color,
    required this.onTap
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 8.0),
      child: SizedBox(
        width: double.maxFinite,
        height: 40,
        child: FloatingActionButton(
            highlightElevation: 0,
            elevation: 0,
            hoverElevation: 0,
            splashColor: Colors.transparent,
            hoverColor: color.withOpacity(0.1),
            backgroundColor: Colors.transparent,
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(6)
            ),
            onPressed: onTap,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Row(
                  children: <Widget>[
                    Icon(
                      options.icon,
                      color: color,
                    ),
                    Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 5),
                      child: Text(
                        options.title,
                        style: TextStyle(color: color),
                      ),
                    ),
                  ],
                ),
                Visibility(
                  visible: options.suffix,
                  child: Icon(
                    Icons.chevron_right_outlined,
                    color: color,
                  ),
                )
              ],
            )),
      ),
    );
  }
}