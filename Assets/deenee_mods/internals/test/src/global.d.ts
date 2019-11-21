interface hooksInterface {
    addListener(event: string, listener: (...args: any[]) => void): this
    on(event: string, listener: (...args: any[]) => void): this
    once(event: string, listener: (...args: any[]) => void): this
    removeListener(event: string, listener: (...args: any[]) => void): this
    removeAllListeners(event: string): this
    listeners(event: string): ((...args: any[]) => void)[]
    emit(event: string, ...args: any[]): boolean;
}
declare var hooks: hooksInterface
declare class Color {
    r: number;
    g: number;
    b: number;
    a: number;
    static red: Color;
    static green: Color;
    static blue: Color;
    static white: Color;
    static black: Color;
    static yellow: Color;
    static cyan: Color;
    static magenta: Color;
    static gray: Color;
    static grey: Color;
    static clear: Color;
    grayscale: number;
    linear: Color;
    gamma: Color;
    maxColorComponent: number;
    item: number;
}
declare class Vector3 {
    static kEpsilon: number;
    static kEpsilonNormalSqrt: number;
    x: number;
    y: number;
    z: number;
    item: number;
    normalized: Vector3;
    magnitude: number;
    sqrMagnitude: number;
    static zero: Vector3;
    static one: Vector3;
    static forward: Vector3;
    static back: Vector3;
    static up: Vector3;
    static down: Vector3;
    static left: Vector3;
    static right: Vector3;
    static positiveInfinity: Vector3;
    static negativeInfinity: Vector3;
    static fwd: Vector3;
    public constructor(x: number, y: number, z: number);
}
declare class IMatrix4x4 {
    m00: number;
    m10: number;
    m20: number;
    m30: number;
    m01: number;
    m11: number;
    m21: number;
    m31: number;
    m02: number;
    m12: number;
    m22: number;
    m32: number;
    m03: number;
    m13: number;
    m23: number;
    m33: number;
    rotation: IQuaternion;
    lossyScale: IVector3;
    isIdentity: boolean;
    determinant: number;
    decomposeProjection: IFrustumPlanes;
    inverse: IMatrix4x4;
    transpose: IMatrix4x4;
    item: number;
    static zero: IMatrix4x4;
    static identity: IMatrix4x4;
}
declare class IFrustumPlanes {
    left: number;
    right: number;
    bottom: number;
    top: number;
    zNear: number;
    zFar: number;
}
declare class IQuaternion {
    x: number;
    y: number;
    z: number;
    w: number;
    static kEpsilon: number;
    item: number;
    static identity: IQuaternion;
    eulerAngles: IVector3;
    normalized: IQuaternion;
}
declare class IVector3 {
    static kEpsilon: number;
    static kEpsilonNormalSqrt: number;
    x: number;
    y: number;
    z: number;
    item: number;
    normalized: IVector3;
    magnitude: number;
    sqrMagnitude: number;
    static zero: IVector3;
    static one: IVector3;
    static forward: IVector3;
    static back: IVector3;
    static up: IVector3;
    static down: IVector3;
    static left: IVector3;
    static right: IVector3;
    static positiveInfinity: IVector3;
    static negativeInfinity: IVector3;
    static fwd: IVector3;
}
declare class Component {
    transform: Transform;
    gameObject: GameObject;
    tag: string;
    rigidbody: Component;
    rigidbody2D: Component;
    camera: Component;
    light: Component;
    animation: Component;
    constantForce: Component;
    renderer: Component;
    audio: Component;
    guiText: Component;
    networkView: Component;
    guiElement: Component;
    guiTexture: Component;
    collider: Component;
    collider2D: Component;
    hingeJoint: Component;
    particleSystem: Component;
    name: string;
    hideFlags: HideFlags;
}
declare class GameObject {
    transform: Transform;
    layer: number;
    active: boolean;
    activeSelf: boolean;
    activeInHierarchy: boolean;
    isStatic: boolean;
    tag: string;
    gameObject: GameObject;
    rigidbody: Component;
    rigidbody2D: Component;
    camera: Component;
    light: Component;
    animation: Component;
    constantForce: Component;
    renderer: Component;
    audio: Component;
    guiText: Component;
    networkView: Component;
    guiElement: Component;
    guiTexture: Component;
    collider: Component;
    collider2D: Component;
    hingeJoint: Component;
    particleSystem: Component;
    name: string;
    hideFlags: HideFlags;
}
declare enum HideFlags {
    None = 0,
    HideInHierarchy = 1,
    HideInInspector = 2,
    DontSaveInEditor = 4,
    NotEditable = 8,
    DontSaveInBuild = 16,
    DontUnloadUnusedAsset = 32,
    DontSave = 52,
    HideAndDontSave = 61,
}
declare class Transform extends Component {
    position: IVector3;
    localPosition: IVector3;
    eulerAngles: IVector3;
    localEulerAngles: IVector3;
    right: IVector3;
    up: IVector3;
    forward: IVector3;
    rotation: IQuaternion;
    localRotation: IQuaternion;
    localScale: IVector3;
    parent: Transform;
    worldToLocalMatrix: IMatrix4x4;
    localToWorldMatrix: IMatrix4x4;
    root: Transform;
    childCount: number;
    lossyScale: IVector3;
    hasChanged: boolean;
    hierarchyCapacity: number;
    hierarchyCount: number;
}
declare namespace ui {
    abstract class UIElement {
        public getParent(): UIElement | null;
        public addChild(el: UIElement): UIElement;
        public transform: Transform;
    }
    class Canvas extends UIElement {
        public constructor();
    }
    class Text extends UIElement {
        public constructor(text?: string);
    }
    class Component<T> extends UIElement {
        public props: T;
        private constructor();
    }
    function createComponent<T>(load: (props: T) => UIElement, props: T): {
        construct(): Component<T>
    }
}