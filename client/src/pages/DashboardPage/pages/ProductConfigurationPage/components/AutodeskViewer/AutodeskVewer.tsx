import { FC, useEffect } from 'react';

interface IAutodeskProps {
  urn: string,
  elementId: string
}

const AutodeskViewer: FC<IAutodeskProps> = ({urn, elementId}) => {

  useEffect(() => {
    startViewing()
  }, [urn])

  const startViewing = () => {
    const element = document.getElementById(elementId)
    if(!element) return

    window.Autodesk.Viewing.Initializer({env: 'Local'}, async function() {
      const viewer = new Autodesk.Viewing.GuiViewer3D(element);
      viewer.start(urn);
      // viewer.loadModel(urn);
    });
  }

  return (
    <div id={elementId}>
    </div>
  )
}

export default AutodeskViewer