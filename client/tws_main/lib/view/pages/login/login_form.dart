part of './login_page.dart';

class _LoginForm extends StatelessWidget {
  const _LoginForm();

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      children: <Widget>[
        Material(
          color: Colors.transparent,
          child: SizedBox(
            width: 250,
            height: 50,
            child: TextField(
              autofocus: true,
              decoration: InputDecoration(
                hintText: 'Input your name',
                hintStyle: TextStyle(
                  color: Colors.lightGreen.shade200.withOpacity(.4),
                ),
                enabledBorder: OutlineInputBorder(
                  borderSide: BorderSide(
                    color: Colors.purple.withOpacity(.7),
                    width: 2,
                  ),
                ),
                focusedBorder: const OutlineInputBorder(
                  borderSide: BorderSide(
                    color: Colors.purple,
                    width: 2,
                  ),
                ),
              ),
            ),
          ),
        )
      ],
    );
  }
}
