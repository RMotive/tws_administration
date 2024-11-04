part of '../trucks_create_whisper.dart';

Future<void> _exceptionDialog(BuildContext ctx, String title){
  return showDialog(
    context: ctx,
    builder: (BuildContext context) {
      return TWSConfirmationDialog(
        showCancelButton: false,
        accept: 'Aceptar',
        title: 'ha ocurrido error fatal al crear $title',
        statement: const Text.rich(
          textAlign: TextAlign.center,
          TextSpan(
            text: 'Ha ocurrido un error inesperado, por favor intentelo de nuevo. \nEn caso de que el problema persista, contacte al administrador.'
          ),
        ),
        onAccept: () {
          Navigator.of(context).pop();
        },
      );
    },
  );
}

Future<void> _failureDialog(BuildContext ctx, String error, String title, List<Object> failures){
  return showDialog(
    context: ctx,
    builder: (BuildContext context) {
      return TWSConfirmationDialog(
        showCancelButton: false,
        accept: 'Aceptar',
        title: 'ha ocurrido un problema al generar $title',
        statement: Text.rich(
          textAlign: TextAlign.center,
          TextSpan(
            text: "$error \n\n",
            children: <InlineSpan>[
              if(failures.isNotEmpty)
              const TextSpan(
                text: "The following records cannot be created, please verify the data and retry the operation with this specific records: \n\n",
                style: TextStyle(fontWeight: FontWeight.w600),
              ),
              if(failures.isNotEmpty)
              for (int i = 0; i < failures.length; i++)
                TextSpan(
                  text: failures[i] is SetOperationFailure<Truck>? "${i + 1} - Truck with Economic number:  ${(failures[i] as SetOperationFailure<Truck>).set.truckCommonNavigation?.economic}\n"
                  : failures[i] is SetOperationFailure<TruckExternal>? "${i + 1} - External Truck with Economic number:  ${(failures[i] as SetOperationFailure<TruckExternal>).set.truckCommonNavigation?.economic}\n"
                  : "${failures[i].runtimeType} : error on load record data.\n",
                  style: const TextStyle(fontWeight: FontWeight.w600),
                ),
            ],
          )
        ),
        onAccept: () {
          Navigator.of(context).pop();
        },
      );
    },
  );
}

Future<void> _conectionDialog(BuildContext ctx, String title) {
  return showDialog(
    context: ctx,
    builder: (BuildContext context) {
      return TWSConfirmationDialog(
        showCancelButton: false,
        accept: 'Aceptar',
        title: 'Problemas de conexión al crear $title',
        statement: const Text.rich(
          textAlign: TextAlign.center,
          TextSpan(
            text: 'No se pudo contactar con el servidor.\n Verifique su conexión a internet o contacte a su administrador.',
          ),
        ),
        onAccept: () {
          Navigator.of(context).pop();
        },
      );
    },
  );
}
