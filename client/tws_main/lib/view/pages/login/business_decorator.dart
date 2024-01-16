part of './login_page.dart';

class _BusinessDecorator extends StatelessWidget {
  const _BusinessDecorator();

  @override
  Widget build(BuildContext context) {
    return Center(
      child: ConstrainedBox(
        constraints: const BoxConstraints(
          minWidth: 200,
          maxWidth: 350,
        ),
        child: const AspectRatio(
          aspectRatio: 1 / .75,
          child: Placeholder(
            child: Center(
              child: Padding(
                padding: EdgeInsets.all(20.0),
                child: Text(
                  'Placeholder for business decorator',
                  textAlign: TextAlign.center,
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
